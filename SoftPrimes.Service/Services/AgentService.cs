using AutoMapper;
using SoftPrimes.BLL.BaseObjects.RepositoriesInterfaces;
using SoftPrimes.Service.IServices;
using SoftPrimes.Shared.Domains;
using SoftPrimes.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SoftPrimes.Service.Services
{
    public class AgentService : BusinessService<Agent, AgentDTO>, IAgentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IRepository<Agent> _repository;
        public AgentService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<Agent>();
        }

        public AgentDTO AddUserImage(string userId, byte[] profileImage)
        {
            MemoryStream ms = new MemoryStream(profileImage);
            Image newImage = GetReducedImage(100, 100, ms);
            byte[] ProfileImageThumbnail = ImageToByteArray(newImage);
            AgentDTO userDetailsDTO = new AgentDTO();
            Agent currentUser = _unitOfWork.GetRepository<Agent>().GetById(userId);
          //  Agent previousUser = currentUser.ShallowCopy();
            currentUser.Id = userId;
            currentUser.Image = ProfileImageThumbnail;
            _UnitOfWork.GetRepository<Agent>().Update(currentUser);
            return _Mapper.Map(currentUser, userDetailsDTO);
        }
        public byte[] ImageToByteArray(Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            if (imageIn != null)
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            }
            return ms.ToArray();
        }
        public Image GetReducedImage(int width, int height, Stream resourceImage)
        {
            try
            {
                Image image = Image.FromStream(resourceImage);
                Image thumb = image.GetThumbnailImage(width, height, () => false, IntPtr.Zero);

                return thumb;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public AgentDTO GetByUserName(string username, bool decrpte)
        {

            var User = _repository.GetAll(false).FirstOrDefault(x => x.UserName.ToUpper() == username.ToUpper());

            if (!string.IsNullOrEmpty(username))
            {
                if (User != null)
                {
                    string UserId = User.Id;
                    AgentDTO user = GetDetails(UserId);
                    return user;

                }
            }
            return null;
        }

        public IEnumerable<AgentDTO> InsertNewUsers(IEnumerable<AgentDTO> entities)
        {
            List<Agent> users = new List<Agent>();
            foreach (AgentDTO user in entities)
            {
                Agent New_user = _Mapper.Map(user, typeof(AgentDTO), typeof(Agent)) as Agent;
                New_user.Id = Guid.NewGuid().ToString();
                Agent mm = _repository.Insert(New_user);
                users.Add(mm);

            }
            return _Mapper.Map(users, typeof(List<Agent>), typeof(List<AgentDTO>)) as List<AgentDTO>;
        }


        public string ModifyProfileImages()
        {
            var users = _repository.GetAll().Select(x => new
            {
                userId = x.Id,
                ProfileImage = x.Image,
            }).ToList();
            foreach (var item in users)
            {
                try
                {
                    if (item.ProfileImage != null)
                    {
                        this.AddUserImage(item.userId, item.ProfileImage);
                    }
                }
                catch (Exception)
                {


                }

            }
            return "Profile Images Updated ";
        }
        public IEnumerable<AgentDTO> UpdateUsers(IEnumerable<AgentDTO> entities)
        {
        
            foreach (var Entity in entities)
            {
                var OldEntity = this._UnitOfWork.GetRepository<Agent>().GetById(Entity.Id);
                byte[] profileImage = OldEntity.Image;
                Agent MappedEntity = _Mapper.Map(Entity, OldEntity, typeof(AgentDTO), typeof(Agent)) as Agent;
                MappedEntity.Image = profileImage;
                this._UnitOfWork.GetRepository<Agent>().Update(MappedEntity as Agent);
            }
            return entities;
        }
        //private void CustomeMethod()
        //{
        //    var company=_repository.
        //}
    }
}