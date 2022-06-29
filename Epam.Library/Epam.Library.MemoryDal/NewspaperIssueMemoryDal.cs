using Epam.Library.DalContracts;
using Epam.Library.Entities;
using Epam.Library.Entities.Exceptions;
using System.Linq;

namespace Epam.Library.MemoryDal
{
    public class NewspaperIssueMemoryDal : INewspaperIssueDal
    {
        public int Add(int newspaperId, NewspaperIssue newspaperIssue)
        {
            NewspaperIssue saveNewspaperIssue = new NewspaperIssue
            {
                Number = newspaperIssue.Number,
                PublishingDate = newspaperIssue.PublishingDate
            };

            saveNewspaperIssue.Id = Memory.NextId;

            Newspaper newspaperForAddNewspaperIssue = Memory.LibraryObjects
                .Where(p => p is Newspaper && p.Id == newspaperId)
                .Select(p => (Newspaper)p).FirstOrDefault();

            int countIssues = newspaperForAddNewspaperIssue.NewspaperIssues.Count;

            if (Uniqueness(newspaperForAddNewspaperIssue, saveNewspaperIssue))
            {
                newspaperForAddNewspaperIssue.NewspaperIssues.Add(saveNewspaperIssue);
            }

            if (newspaperForAddNewspaperIssue.NewspaperIssues.Count > countIssues)
                return newspaperForAddNewspaperIssue.Id;
            else
                throw new ObjectNotUniqueException();
        }

        public bool CheckNewspaperIssueLikeDeletedById(int newspaperId, int newspaperIssueId)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(int newspaperId, int newspaperIssueId)
        {
            throw new System.NotImplementedException();
        }

        public bool RestoreNewspaperIssue(int newspaperId, int newspaperIssueId)
        {
            throw new System.NotImplementedException();
        }

        private bool Uniqueness(Newspaper newspaper, NewspaperIssue newspaperIssue)
        {
            return !newspaper.NewspaperIssues.Any(n => n.PublishingDate.ToShortDateString().Equals(newspaperIssue.PublishingDate.ToShortDateString()));
        }
    }
}
