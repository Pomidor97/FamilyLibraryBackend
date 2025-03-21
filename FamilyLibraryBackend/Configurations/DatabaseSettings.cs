﻿namespace FamilyLibraryBackend.Configurations;

public class DatabaseSettings
{
    public string ConnectionString { get; set; } = string.Empty;
    public string DatabaseName { get; set; } = string.Empty;
    public string FamiliesCollectionName { get; set; } = string.Empty;
}
