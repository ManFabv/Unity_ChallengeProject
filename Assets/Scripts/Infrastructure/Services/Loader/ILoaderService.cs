﻿namespace PPop.Infrastructure.Services.Loader
{
     public interface ILoaderService
     {
          T Read<T>(string fileName);
     }
}