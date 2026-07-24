namespace DopeWars.Components;

public class CustomButton : Button
{
    private const double PressedScale = 0.90;
    private const uint PressDuration = 250;
    private const uint ReleaseDuration = 350;

    public CustomButton()
    {
        Pressed += OnPressed;
        Released += OnReleased;
    }

    private void OnPressed(object? sender, EventArgs e)
    {
        this.AbortAnimation(nameof(CustomButton));

        _ = this.ScaleToAsync(
            PressedScale,
            PressDuration,
            Easing.CubicOut);
    }

    private void OnReleased(object? sender, EventArgs e)
    {
        _ = this.ScaleToAsync(
            1.0,
            ReleaseDuration,
            Easing.SpringOut);
    }
}
