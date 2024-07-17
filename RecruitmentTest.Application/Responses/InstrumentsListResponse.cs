namespace RecruitmentTest.Application.Responses
{
    public class InstrumentsListResponse
    {
        public List<InstrumentResponse>? Data { get; set; }
    }
    public class InstrumentResponse
    {
        public Guid Id { get; set; }
        public string? Symbol { get; set; }
        public string? Kind { get; set; }
        public string? Description { get; set; }
        public double TickSize { get; set; }
        public string? Currency { get; set; }
        public string? BaseCurrency { get; set; }
        public Dictionary<string, MappingResponse>? Mappings { get; set; }
    }

    public class MappingResponse
    {
        public string? Symbol { get; set; }
        public string? Exchange { get; set; }
        public int? DefaultOrderSize { get; set; }
    }
}
