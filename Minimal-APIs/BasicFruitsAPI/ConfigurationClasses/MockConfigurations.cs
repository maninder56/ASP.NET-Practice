namespace BasicFruitsAPI.ConfigurationClasses;

public class MockConfigurations
{
    public ComputerInfoClass? ComputerInfo { get; set; } 

    public class ComputerInfoClass
    {
        public string? Storage { get; set; }

        public int RAM { get; set; }

        public string? CPU { get; set; }
    }
}

