//-----------------------------------------------------------------------
// <copyright file="HotKeyMap.cs" company="Gavin Kendall">
//     Copyright (c) 2020 Gavin Kendall
// </copyright>
// <author>Gavin Kendall</author>
// <summary>Methods for registering keyboard shortcuts and defining modifier keys.</summary>
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program. If not, see <https://www.gnu.org/licenses/>.
//-----------------------------------------------------------------------
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AutoScreenCapture
{
    /// <summary>
    /// A class for registering and unregistering keyboard shortcuts.
    /// </summary>
    public sealed class HotKeyMap : IDisposable
    {
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);
        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private Window _window = new Window();
        private int _currentId;

        private class Window : NativeWindow, IDisposable
        {
            private static int WM_HOTKEY = 0x0312;

            public Window()
            {
                CreateHandle(new CreateParams());
            }

            protected override void WndProc(ref Message msg)
            {
                base.WndProc(ref msg);

                if (msg.Msg == WM_HOTKEY)
                {
                    Keys key = (Keys)(((int)msg.LParam >> 16) & 0xFFFF);
                    ModifierKeys modifier = (ModifierKeys)((int)msg.LParam & 0xFFFF);

                    KeyPressed?.Invoke(this, new KeyPressedEventArgs(modifier, key));
                }
            }

            public event EventHandler<KeyPressedEventArgs> KeyPressed;

            public void Dispose()
            {
                DestroyHandle();
            }
        }

        /// <summary>
        /// Constructor for the keyboard shortcuts class.
        /// </summary>
        public HotKeyMap()
        {
            _window.KeyPressed += delegate (object sender, KeyPressedEventArgs args)
            {
                KeyPressed?.Invoke(this, args);
            };
        }

        /// <summary>
        /// Registers a keyboard shortcut.
        /// </summary>
        /// <param name="modifier">The modifier keys to register.</param>
        /// <param name="key">The key to register.</param>
        public void RegisterHotKey(ModifierKeys modifier, Keys key)
        {
            _currentId += 1;

            if (!RegisterHotKey(_window.Handle, _currentId, (uint)modifier, (uint)key))
                throw new InvalidOperationException("Unable to register hot key");
        }

        /// <summary>
        /// An event handler that triggers when a key is pressed.
        /// </summary>
        public event EventHandler<KeyPressedEventArgs> KeyPressed;

        /// <summary>
        /// Unregisters the keyboard shortcuts on dispose.
        /// </summary>
        public void Dispose()
        {
            for (int i = _currentId; i > 0; i--)
            {
                UnregisterHotKey(_window.Handle, i);
            }

            _window.Dispose();
        }
    }

    /// <summary>
    /// A class to handle key presses.
    /// </summary>
    public class KeyPressedEventArgs : EventArgs
    {
        internal KeyPressedEventArgs(ModifierKeys modifier, Keys key)
        {
            Modifier = modifier;
            Key = key;
        }

        public ModifierKeys Modifier { get; }

        public Keys Key { get; }
    }

    /// <summary>
    /// A list of available modifier keys.
    /// </summary>
    [Flags]
    public enum ModifierKeys : uint
    {
        Alt = 1,
        Control = 2,
        Shift = 4,
        Win = 8
    }
}
