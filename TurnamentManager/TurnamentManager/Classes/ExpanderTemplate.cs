using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace TurnamentManager.Classes
{
    public class ExpanderTemplate
    {
        public event EventHandler Changed; 
        public bool IsExpanded { get; private set; }
        
        public int SelectedId { get; private set; }
        
        public List<ImageSource> ImageSources { get; private set; }
        
        public Command Command { get; private set; }

        public ImageSource CurrentImageSource
        {
            get => _currentImageSource;
            private set
            {
                if (value == _currentImageSource)
                    return;

                _currentImageSource = value;
                Changed?.Invoke(this, EventArgs.Empty);
            }
        }

        private ImageSource _currentImageSource;

        public ExpanderTemplate(List<ImageSource> images) //Image 0 is default image
        {
            IsExpanded = true;
            SelectedId = -1;
            ImageSources = images;
            Command = new Command<int>(DoCommand);
            CurrentImageSource = ImageSources[0];
            Changed?.Invoke(this, EventArgs.Empty);
        }

        private void DoCommand(int selectedId)
        {
            IsExpanded = false;
            SelectedId = selectedId;
            CurrentImageSource = ImageSources[selectedId + 1];
            Changed?.Invoke(this, EventArgs.Empty);
        }
    }
}
