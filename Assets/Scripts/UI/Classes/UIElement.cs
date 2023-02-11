namespace Lindon.UserManager.Base.Element
{
    public abstract class UIElement : UIClass
    {
        public abstract void DoCreate();
        public virtual void OnDestory()
        {

        }
    }
}