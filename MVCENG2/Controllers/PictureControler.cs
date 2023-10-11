using Microsoft.AspNetCore.Mvc;
using HoffmanWebstatistic.Interfaces;
using HoffmanWebstatistic.Models.General;
using HoffmanWebstatistic.Repository;
using Microsoft.AspNetCore.Authorization;
using HoffmanWebstatistic.Services;
using HoffmanWebstatistic.Models.ViewModel;
using HoffmanWebstatistic.Models.Hoffman;
using System.Drawing;

namespace HoffmanWebstatistic.Controllers
{
    [Authorize(Roles = "sa, admin")]
    public class PictureController : Controller
    {
        private readonly PictureRepository _pictureRepository;
        private readonly StandRepository _standRepository;
        private readonly UsersRepository _usersRepository;
        private readonly SendingStatusLogRepository _sendingStatusLogRepository;

        public PictureController(PictureRepository pictureRepository, StandRepository standRepository, UsersRepository usersRepository, SendingStatusLogRepository sendingStatusLogRepository)
        {
            _pictureRepository = pictureRepository;
            _standRepository = standRepository;
            _usersRepository = usersRepository;
            _sendingStatusLogRepository = sendingStatusLogRepository;
        }

        public async Task<IActionResult> MainMenu()
        {
            List<Picture> pictureList = new List<Picture>();

            var allPictures = _pictureRepository.GetAll();

            foreach (Picture picture in allPictures)
            {
                using (MemoryStream memoryStream = new MemoryStream(picture.PictureBytes))
                {
                    using (Image originalImage = Image.FromStream(memoryStream))
                    {
                        int newWidth = 200; // Желаемая ширина изображения
                        int newHeight = 70; // Желаемая высота изображения

                        Image resizedImage = new Bitmap(newWidth, newHeight);
                        using (Graphics graphics = Graphics.FromImage(resizedImage))
                        {
                            graphics.DrawImage(originalImage, 0, 0, newWidth, newHeight);
                        }

                        using (MemoryStream resizedMemoryStream = new MemoryStream())
                        {
                            resizedImage.Save(resizedMemoryStream, originalImage.RawFormat);
                            picture.PictureBytes = resizedMemoryStream.ToArray();
                            pictureList.Add(picture);
                            
                            //byte[] resizedPicture = resizedMemoryStream.ToArray();
                            
                        }
                    }

                }
            }
            
            return View(pictureList);
        }

        [HttpGet]
        public async Task<IActionResult> AddPicture()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPicture(List<IFormFile> files)
        {
            foreach (var file in files)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    byte[] fileBytes = memoryStream.ToArray();

                    string prefix = "ppd_";
                    string fileName = file.FileName;
                    if (!fileName.StartsWith(prefix))
                    {
                        fileName = prefix + fileName;
                    }
                    Picture picture = new Picture { PName = fileName, PictureBytes = fileBytes };
                    
                    _pictureRepository.Add(picture);


                    int userId = _usersRepository.GetUserByName(HttpContext.User.Identity.Name).Id;
                    InteractionStand interactionStand = new InteractionStand(_standRepository, _pictureRepository, _sendingStatusLogRepository);
                    interactionStand.AddPictureForStands(picture, userId);                
                                                                                             
                }
                
            }

            return RedirectToAction("MainMenu");
          
        }


        [HttpGet]
        public async Task<IActionResult> EditPicture(int pictureId)
        {
                      
            var pictureObject = _pictureRepository.GetPictureById(pictureId);
            
            return View(pictureObject);

        }

        [HttpPost]
        public async Task<IActionResult> EditPicture(string oldPictureName, string newPictureName, IFormFile file)
        {
            InteractionStand interactionStand = new InteractionStand(_standRepository, _pictureRepository, _sendingStatusLogRepository);
            Picture newPicture = new Picture();


            if (file == null) 
            {
                newPicture = _pictureRepository.EditPicture(oldPictureName, newPictureName, null);
            }
            else
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    byte[] fileBytes = memoryStream.ToArray();
                    newPicture = _pictureRepository.EditPicture(oldPictureName,newPictureName,fileBytes);
                    
                }
            }

            int userId = _usersRepository.GetUserByName(HttpContext.User.Identity.Name).Id;

            interactionStand.DeletePictureFromStands(oldPictureName, userId);
            interactionStand.EditPictureFromStands(newPicture, userId);
                       
            return RedirectToAction("MainMenu");            
        }

        [HttpGet]
        public async Task<IActionResult> DeletePicture(string pictureName)
        {
            int userId = _usersRepository.GetUserByName(HttpContext.User.Identity.Name).Id;

            InteractionStand interactionStand = new InteractionStand(_standRepository, _pictureRepository, _sendingStatusLogRepository);
            interactionStand.DeletePictureFromStands(pictureName, userId);

            _pictureRepository.Delete(pictureName);

            return RedirectToAction("MainMenu");
            
        }
        
     
    }

}

