using Flipify.ViewModel;

namespace Flipify.Model;

public partial class Card : BaseVM
{
    private string _front;
    public string Front
    {
        get => _front;
        set
        {
            if (_front != value)
            {
                _front = value;
                OnPropertyChanged(Front);
                OnPropertyChanged(DisplayText);
            }
        }
    }

    private string _back;
    public string Back
    {
        get => _back;
        set
        {
            if (_back != value)
            {
                _back = value;
                OnPropertyChanged(Back);
                OnPropertyChanged(DisplayText);
            }
        }
    }

    private bool _isFlipped;
    public bool IsFlipped
    {
        get => _isFlipped;
        set
        {
            if (_isFlipped != value)
            {
                _isFlipped = value;
                OnPropertyChanged(nameof(IsFlipped));
                OnPropertyChanged(nameof(DisplayText));
            }
        }
    }
    public string DisplayText => IsFlipped ? Back : Front;

}
