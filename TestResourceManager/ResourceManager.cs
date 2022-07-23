namespace TestResourceManager
{
    public class ResourceManager
    {
        public string ResourcesPath { get; }
        public DirectoryInfo Resources { get; }

        public ResourceManager()
        {
            var lookingFor = @"\AdHocSensors\";
            var bin = Directory.GetCurrentDirectory();
            var root = bin.Substring(0, bin.IndexOf(lookingFor) + lookingFor.Length);

            ResourcesPath = Path.Combine(root, @"TestResourceManager\Resources");
            Resources = new DirectoryInfo(ResourcesPath);
        }

        public string GetFile(string fileName)
        {
            return Resources.GetFiles(fileName).FirstOrDefault().FullName;
        }
    }
}