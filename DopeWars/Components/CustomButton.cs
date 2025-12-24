namespace DopeWars.Components;

public class CustomButton : Button
{
    // Example: add a custom property
    public static readonly BindableProperty CornerRadiusProperty =
        BindableProperty.Create(
            nameof(CornerRadius),
            typeof(int),
            typeof(CustomButton),
            0);

    public int CornerRadius
    {
        get => (int)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    // You can override other behavior if necessary
}
