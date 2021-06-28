using System;

public class EventHandler : Singleton<EventHandler>
{
    #region Input
    public event Action JumpInputEvent;
    public void InvokeJumpInput()
    {
        JumpInputEvent?.Invoke();
    }

    public event Action SwitchSideInputEvent;
    public void InvokeSwitchSideInput()
    {
        SwitchSideInputEvent?.Invoke();
    }
    #endregion

    #region SwitchindSides
    public event Action<Sides> SideSwitchedEvent;
    public void InvokeSideSwitched(Sides side)
    {
        SideSwitchedEvent?.Invoke(side);
    }

    #endregion

    #region SimpleInteracts
    public event Action SimpleButtonPressedEvent;
    public void InvokeSimpleButtonPressed()
    {
        SimpleButtonPressedEvent?.Invoke();
    }


    #endregion

    #region ProvidedInterats
    public event Action FirstStonePlacedEvent;
    public void InvokeFirstStonePlaced()
    {
        FirstStonePlacedEvent?.Invoke();
    }
    // -------------------------------------------------- //
    public event Action SecondStonePlacedEvent;
    public void InvokeSecondStonePlaced()
    {
        SecondStonePlacedEvent?.Invoke();
    }


    #endregion
}
