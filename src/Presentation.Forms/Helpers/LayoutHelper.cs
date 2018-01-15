// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for details.

using System;
using System.Windows.Forms;

namespace Platform.Presentation.Forms
{
    public class LayoutHelper
    {
        public static IDisposable SuspendAnchoring(params Control[] controls)
        {
            return new AnchorSuspension(controls);
        }

        private class AnchorSuspension : IDisposable
        {
            private Control[] _controls;
            private readonly AnchorStyles[] _anchorStyles;

            public AnchorSuspension(Control[] controls)
            {
                _anchorStyles = new AnchorStyles[controls.Length];
                _controls = controls;
                for (int i = 0; i < _controls.Length; i++)
                {
                    Control c = _controls[i];
                    if (c != null)
                    {
                        _anchorStyles[i] = c.Anchor;
                        c.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                    }
                }
            }

            public void Dispose()
            {
                Control[] controls = _controls;
                _controls = null;
                if (controls != null)
                {
                    for (int i = 0; i < controls.Length; i++)
                    {
                        Control c = controls[i];
                        if (c != null)
                        {
                            c.Anchor = _anchorStyles[i];
                        }
                    }
                }
            }
        }
    }
}