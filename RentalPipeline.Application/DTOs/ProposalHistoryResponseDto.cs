namespace RentalPipeline.Application.DTOs
{
    public class ProposalHistoryResponseDto
    {
        public string PreviousStatus { get; set; }
        public string CurrentStatus { get; set; }
        public DateTime ChangedAt { get; set; }
    }
}
