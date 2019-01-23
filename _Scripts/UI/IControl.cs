namespace MyShooter.UI
{
    public interface IControl
    {
        UnityEngine.GameObject Instance { get; }
        UnityEngine.UI.Selectable Control { get; }
        UnityEngine.UI.Text GetText { get; }
    }
}