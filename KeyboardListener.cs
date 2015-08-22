//////////////////////////////////////////////////////////
// Auto Screen Capture 2.0.5
// autoscreen.KeyboardListener.cs
//
// Written by Gavin Kendall (gavinkendall@gmail.com)
// Thursday, 15 May 2008 - Wednesday, 27 November 2013

using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace autoscreen
{
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand)]
    public class KeyboardListener
    {
        public static bool keyShift;
        public static int keyUp = 257;
        public static int keyDown = 256;

        private static ListeningWindow listeningWindow;
        public static event EventHandler eventHandler;

        private static void KeyHandler(ushort key, uint message)
        {
            if (eventHandler != null)
            {
                Delegate[] delegates = eventHandler.GetInvocationList();

                foreach (Delegate del in delegates)
                {
                    EventHandler eventHandler = (EventHandler)del;
                    eventHandler(null, new KeyboardEventArgs(key, message));
                }
            }
        }

        public class KeyboardEventArgs : KeyEventArgs
        {
            public uint m_message;
            public ushort m_key;
            public bool m_shift;

            public KeyboardEventArgs(ushort key, uint message) : base((Keys)key)
            {
                m_message = message;
                m_key = key;
            }
        }

        static KeyboardListener()
        {
            ListeningWindow.KeyDelegate keyDelegate = new ListeningWindow.KeyDelegate(KeyHandler);
            listeningWindow = new ListeningWindow(keyDelegate);
        }

        private class ListeningWindow : NativeWindow
        {
            [DllImport("User32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern unsafe bool RegisterRawInputDevices(RAWINPUTDEV* rawInputDevices, uint numDevices, uint size);

            [DllImport("User32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
            [return: MarshalAs(UnmanagedType.I4)]
            internal static extern unsafe int GetRawInputData(void* hRawInput, uint uiCommand, byte* pData, uint* pcbSize, uint cbSizeHeader);

            public delegate void KeyDelegate(ushort key, uint message);

            private const int
                WS_CLIPCHILDREN = 0x02000000,
                WM_INPUT = 0x00FF,
                RIDEV_INPUTSINK = 0x00000100,
                RID_INPUT = 0x10000003,
                RIM_TYPEKEYBOARD = 1;

            private uint m_previousMessage = 0;
            private ushort m_previousControlKey = 0;
            private KeyDelegate m_KeyHandler = null;

            internal unsafe struct RAWINPUTDEV
            {
                public ushort usUsagePage;
                public ushort usUsage;
                public uint dwFlags;
                public void* hwndTarget;
            };

            internal unsafe struct RAWINPUTHEADER
            {
                public uint dwType;
                public uint dwSize;
                public void* hDevice;
                public void* wParam;
            };

            internal unsafe struct RAWINPUTHKEYBOARD
            {
                public RAWINPUTHEADER header;
                public ushort MakeCode;
                public ushort Flags;
                public ushort Reserved;
                public ushort VKey;
                public uint Message;
                public uint ExtraInformation;
            };

            public ListeningWindow(KeyDelegate keyHandlerFunction)
            {
                m_KeyHandler = keyHandlerFunction;

                CreateParams createParams = new CreateParams();

                createParams.Caption = "Hidden window";
                createParams.ClassName = null;
                createParams.X = 0x7FFFFFFF;
                createParams.Y = 0x7FFFFFFF;
                createParams.Height = 0;
                createParams.Width = 0;
                createParams.Style = WS_CLIPCHILDREN;

                this.CreateHandle(createParams);

                unsafe
                {
                    RAWINPUTDEV rawInputDevice = new RAWINPUTDEV();
                    rawInputDevice.usUsagePage = 0x01;
                    rawInputDevice.usUsage = 0x06;
                    rawInputDevice.dwFlags = RIDEV_INPUTSINK;
                    rawInputDevice.hwndTarget = this.Handle.ToPointer();

                    RegisterRawInputDevices(&rawInputDevice, 1, (uint)sizeof(RAWINPUTDEV));
                }
            }

            protected override void WndProc(ref Message m)
            {
                switch (m.Msg)
                {
                    case WM_INPUT:
                        {
                            unsafe
                            {
                                uint dwSize, receivedBytes;
                                uint sizeof_RAWINPUTHEADER = (uint)(sizeof(RAWINPUTHEADER));

                                int res = GetRawInputData(m.LParam.ToPointer(), RID_INPUT, null, &dwSize, sizeof_RAWINPUTHEADER);

                                if (res == 0)
                                {
                                    byte* lpb = stackalloc byte[(int)dwSize];

                                    receivedBytes = (uint)GetRawInputData((RAWINPUTHKEYBOARD*)(m.LParam.ToPointer()), RID_INPUT, lpb, &dwSize, sizeof_RAWINPUTHEADER);

                                    if (receivedBytes == dwSize)
                                    {
                                        RAWINPUTHKEYBOARD* keybData = (RAWINPUTHKEYBOARD*)lpb;

                                        if (keybData->header.dwType == RIM_TYPEKEYBOARD)
                                        {
                                            if ((m_previousControlKey != keybData->VKey) || (m_previousMessage != keybData->Message))
                                            {
                                                m_previousControlKey = keybData->VKey;
                                                m_previousMessage = keybData->Message;

                                                m_KeyHandler(keybData->VKey, keybData->Message);
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        break;
                }

                base.WndProc(ref m);
            }
        }
    }
}
