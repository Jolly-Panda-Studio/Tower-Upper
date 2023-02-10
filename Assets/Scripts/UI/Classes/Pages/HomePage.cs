using Lindon.UserManager;
using Lindon.UserManager.Base.Page;

public class HomePage : UIPage
{
    protected override void SetValues()
    {
        UserInterfaceManager.Open<HUDPage>();
    }

    protected override void SetValuesOnSceneLoad()
    {

    }
}
