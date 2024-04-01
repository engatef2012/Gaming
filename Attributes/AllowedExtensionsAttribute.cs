using System.ComponentModel.DataAnnotations;

namespace Gaming.Attributes
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string _allowedExtensions;

        public AllowedExtensionsAttribute(string allowedExtensions)
        {
            _allowedExtensions = allowedExtensions;
        }
        public override bool IsValid(object? value)
        {
            var file = value as IFormFile;
            if (file is not null)
            {
                var extension = Path.GetExtension(file.FileName);
                bool isAllowed = _allowedExtensions.Split(",").Contains(extension, comparer: StringComparer.OrdinalIgnoreCase);
                if (!isAllowed)
                {
                    ErrorMessage = $"only {_allowedExtensions} are allowed";
                    return false;
                }
                return true;
            }
            return false;
        }
    }
}
