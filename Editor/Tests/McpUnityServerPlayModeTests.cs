using McpUnity.Unity;
using NUnit.Framework;
using UnityEditor;

namespace McpUnity.Tests
{
    public class McpUnityServerPlayModeTests
    {
        private bool _originalEnterPlayModeOptionsEnabled;
        private EnterPlayModeOptions _originalEnterPlayModeOptions;

        [SetUp]
        public void SetUp()
        {
            _originalEnterPlayModeOptionsEnabled = EditorSettings.enterPlayModeOptionsEnabled;
            _originalEnterPlayModeOptions = EditorSettings.enterPlayModeOptions;
        }

        [TearDown]
        public void TearDown()
        {
            EditorSettings.enterPlayModeOptionsEnabled = _originalEnterPlayModeOptionsEnabled;
            EditorSettings.enterPlayModeOptions = _originalEnterPlayModeOptions;
        }

        [Test]
        public void ShouldKeepServerRunningDuringPlayMode_ReturnsFalse_WhenEnterPlayModeOptionsDisabled()
        {
            EditorSettings.enterPlayModeOptionsEnabled = false;
            EditorSettings.enterPlayModeOptions = EnterPlayModeOptions.DisableDomainReload;

            Assert.IsFalse(McpUnityServer.ShouldKeepServerRunningDuringPlayMode());
        }

        [Test]
        public void ShouldKeepServerRunningDuringPlayMode_ReturnsFalse_WhenDomainReloadIsEnabled()
        {
            EditorSettings.enterPlayModeOptionsEnabled = true;
            EditorSettings.enterPlayModeOptions = EnterPlayModeOptions.DisableSceneReload;

            Assert.IsFalse(McpUnityServer.ShouldKeepServerRunningDuringPlayMode());
        }

        [Test]
        public void ShouldKeepServerRunningDuringPlayMode_ReturnsTrue_WhenDomainReloadIsDisabled()
        {
            EditorSettings.enterPlayModeOptionsEnabled = true;
            EditorSettings.enterPlayModeOptions =
                EnterPlayModeOptions.DisableDomainReload |
                EnterPlayModeOptions.DisableSceneReload;

            Assert.IsTrue(McpUnityServer.ShouldKeepServerRunningDuringPlayMode());
        }
    }
}
