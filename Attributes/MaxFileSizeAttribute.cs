using System.ComponentModel.DataAnnotations;

namespace Gaming.Attributes
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="maxFileSize">the maximum file size in MB</param>
        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        public override bool IsValid(object? value)
        {
            var file = value as FormFile;
            if (file is not null)
            {
                if (file.Length > _maxFileSize)
                {
                    ErrorMessage = $"maximum allowed size is {_maxFileSize / 1024 / 1024}MB";
                    return false;
                }
            }
            return true;
        }
    }
}
