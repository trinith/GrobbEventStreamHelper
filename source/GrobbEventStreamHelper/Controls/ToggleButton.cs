using System;
using System.Collections.Generic;

namespace GrobbEventStreamHelper.Controls
{
    public class ToggleButton : Button
    {
        private List<ToggleButton> _linkedButtons = new List<ToggleButton>();

        public bool IsActive { get; private set; } = false;
        public IReadOnlyCollection<ToggleButton> LinkedButtons { get { return _linkedButtons; } }

        public ToggleButton()
        {
        }

        public void LinkTo(ToggleButton button)
        {
            if (button == null)
                throw new ArgumentNullException();

            if (button == this)
                throw new ArgumentException("Cannot link to self.");

            if (_linkedButtons.Contains(button))
                throw new ArgumentException("Button is already linked.");

            _linkedButtons.Add(button);
        }

        public void LinkTo(IEnumerable<ToggleButton> buttons)
        {
            foreach (ToggleButton button in buttons)
            {
                if (button == this)
                    continue;

                this.LinkTo(button);
            }
        }

        public void Activate()
        {
            if (!this.IsActive)
            {
                foreach (ToggleButton button in _linkedButtons)
                    button.Deactivate();

                this.IsActive = true;
            }
        }

        public void Deactivate()
        {
            if (this.IsActive)
                this.IsActive = false;
        }
    }
}
