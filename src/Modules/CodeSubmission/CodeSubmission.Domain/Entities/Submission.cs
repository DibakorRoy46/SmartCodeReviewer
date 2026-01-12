using CodeSubmission.Domain.Enums;
using Shared.Domain.Base;
using Shared.Domain.Exceptions;
using Shared.Domain.Primitives;

namespace CodeSubmission.Domain.Entities;

public class Submission : AggregateRoot<Guid>
{
    public string Code { get; private set; }
    public LanguageEnum Language { get; private set; }
    public SubmissionTypeEnum SubmissionType { get; private set; }
    public string? PlainText { get; private set; }
    public string? FileName { get; private set; }
    public string? FilePath { get; private set; }
    public UserId SubmittedUserId { get; private set; }
    public StatusEnum Status { get; private set; }

    // -------- Required by ORM --------
    private Submission() { }

    private Submission(Guid id,UserId userId,LanguageEnum language,SubmissionTypeEnum submissionType,
        string code,string? plainText,string? fileName,string? filePath)
    {
        Id = id;
        Language = language;
        SubmissionType = submissionType;
        Code = code;
        PlainText = plainText;
        FileName = fileName;
        FilePath = filePath;
        Status = StatusEnum.Submitted;
        SubmittedUserId = userId;

        CreateAudit(userId);
        Validate();
    }

    // FACTORY METHODS (Creation)
   

    public static Submission CreateInline(Guid id, string code,UserId userId,LanguageEnum language,string plainText)
    {
        if (string.IsNullOrWhiteSpace(plainText))
            throw new DomainException("Inline submission requires code.");

        return new Submission(id,userId,language,SubmissionTypeEnum.InlineCode,code, plainText,null,null);
    }

    public static Submission CreateFile(Guid id, string code,UserId userId,LanguageEnum language,
        string fileName,string filePath)
    {
        if (string.IsNullOrWhiteSpace(fileName) || string.IsNullOrWhiteSpace(filePath))
            throw new DomainException("File submission requires file name and path.");

        return new Submission(id,userId,language,SubmissionTypeEnum.File,code,null,fileName,filePath);
    }

    public void MarkInReview(UserId reviewerId)
    {
        EnsureStatus(StatusEnum.Submitted);
        Status = StatusEnum.InReview;
        UpdateAudit(reviewerId);
    }

    public void MarkCompleted(UserId reviewerId)
    {
        EnsureStatus(StatusEnum.InReview);
        Status = StatusEnum.Completed;
        UpdateAudit(reviewerId);
    }

    public void MarkRejected(UserId reviewerId, string? reason = null)
    {
        EnsureNotCompleted();
        Status = StatusEnum.Rejected;
        UpdateAudit(reviewerId);
    }

    private void Validate()
    {
        if (SubmissionType == SubmissionTypeEnum.InlineCode && string.IsNullOrWhiteSpace(PlainText))
            throw new DomainException("Inline submission must have code.");

        if (SubmissionType == SubmissionTypeEnum.File &&
            (string.IsNullOrWhiteSpace(FileName) || string.IsNullOrWhiteSpace(FilePath)))
            throw new DomainException("File submission must contain file information.");
    }

    private void EnsureStatus(StatusEnum expected)
    {
        if (Status != expected)
            throw new DomainException(
                $"Invalid state transition. Expected '{expected}', but current state is '{Status}'.");
    }

    private void EnsureNotCompleted()
    {
        if (Status == StatusEnum.Completed)
            throw new DomainException("Completed submission cannot be modified.");
    }
}
