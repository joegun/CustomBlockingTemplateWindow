# CustomBlockingTemplateWindow
This is a custom blocking template window implementation in XAML and MVVM, which is

1. Going to block the main window, which the popup is launched, only. Basically, it acts as a modal dialog from the window that launch it.

2. Not going to block the other windows. It is a modeless dialog from the other windows.

3. able to interchange the views based on the needed through RegionManager injection.

USAGE

<interactionRequest:BlockingTemplateWindowView Grid.Row="1" DataContext="{Binding Path=BlockingTemplateWindowViewModel}" 
OverlayOnTopOf="{Binding ElementName=PaymentStatusReleaseGrid, Mode=OneWay}" 
IsDisplayed="{Binding Path=IsDisplayed, Mode=OneWay, UpdateSourceTrigger=PropertyChanged}" />
