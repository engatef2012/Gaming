$.validator.addMethod("fileSize", function (value, element, param) {
    return this.optional(element) || element.files[0].size <= param
}, `the maximum allowed file size is @FileSettings.MaxFileSizeInMB` + `MB`);