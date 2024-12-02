
public class ButtonPressedEvent : Unity.Services.Analytics.Event
{
        public ButtonPressedEvent():base("buttonPressed")
        {
        
        }

        public string ActionName { set { SetParameter ("ActionName", value); } }
}
