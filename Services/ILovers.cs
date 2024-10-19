namespace OurStory.Services;

public interface ILovers
{
    Task<IEnumerable<TB_Descriptions>> getAllDescriptions(int LoverId);
    Task<TB_Lovers> UpdateLover(UpdateLoverDTO Lover, bool IsEditDescription);
    Task<TB_Descriptions> UpdateDescription(DescriptionsDTO DescriptionDTO);
    Task<TB_Descriptions> DeleteDescription(int DescriptionId);
}
