using System.Collections.Generic;

namespace SymmetricEncryption.Views
{
    internal enum ViewKey
    {
        None,
        Input,
    }

    internal class ViewHandler
    {
        private readonly Dictionary<ViewKey, View> views;

        public ViewHandler()
        {
            views = new()
            {
                { ViewKey.Input, new EncryptionView(this) },
            };
        }

        public ViewKey CurrentView { get; private set; } = ViewKey.None;

        public void ChangeView(ViewKey viewKey)
        {
            CurrentView = viewKey;
            views[CurrentView].Initialize();
        }

        public View GetCurrentView()
        {
            return views[CurrentView];
        }

        public T GetViewByKey<T>(ViewKey viewKey) where T : View
        {
            return (T)views[viewKey];
        }
    }
}
