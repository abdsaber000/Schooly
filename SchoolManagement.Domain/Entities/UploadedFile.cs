using System;

namespace SchoolManagement.Domain.Entities;

public class UploadedFile
{
    public int Id {get; set;}
    public string FileName {get; set;} = string.Empty;
    public string StoredFileName {get; set;} = string.Empty;
    public string ContentType {get; set;} = string.Empty;
}
