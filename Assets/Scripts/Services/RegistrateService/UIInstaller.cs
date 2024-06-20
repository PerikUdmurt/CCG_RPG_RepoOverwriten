using CCG.Infrastructure.Factory;
using CCG.UI.Hints;
using UnityEngine;
using Zenject;

namespace CCG.Services.Installer
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private RectTransform HintsEntryPos;

        public override void InstallBindings()
        {
            BindFactories();
            BindHints();
        }

        private void BindHints()
        {
            Container.BindInterfacesAndSelfTo<HintService>().AsSingle().NonLazy();
            Container.Bind<RectTransform>().FromInstance(HintsEntryPos).WhenInjectedInto<HintService>();
        }

        private void BindFactories()
        {
            Container.BindFactory<HintUI, CustomFactory<HintUI>>().FromFactory<CustomFactory<HintUI>>().NonLazy();
        }
    }
}
