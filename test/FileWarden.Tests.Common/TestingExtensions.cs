using Moq;

namespace FileWarden.Tests.Common
{
    public static class TestingExtensions
    {
        public static Times OnceOrNever(this bool value) => value ? Times.Once() : Times.Never();
        public static Times NeverOrOnce(this bool value) => value ? Times.Never() : Times.Once();
    }
}
