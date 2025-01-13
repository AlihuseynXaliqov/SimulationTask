namespace SimulationTask.Helper.File
{
    public static class FileExtension
    {
        public static string Upload(this IFormFile formFile,string rootpath, string foldername)
        {
            string fileName= formFile.FileName;
            if(fileName.Length > 64)
            {
                fileName = fileName.Substring(fileName.Length-64);
            }
            fileName=Guid.NewGuid()+ fileName;
            string path= Path.Combine(rootpath, foldername,fileName);
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                formFile.CopyTo(fs);
            }
            return fileName;
        }
    }
}
