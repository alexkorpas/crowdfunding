using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public partial class CrowdFundingTransactions
    {
        public async Task<TransactionResult> SaveImageTransaction(ProjectPhotoDTO projectPhotoDTO, string user)
        {
            try
            {

                AspNetUsers _user = await context.AspNetUsers.Where(u => u.UserName == user).FirstOrDefaultAsync();
                Project _project = null;
                if (projectPhotoDTO.Id != null)
                    _project = await context.Project.FindAsync(projectPhotoDTO.Id);
                if (_project != null)
                    if (_project.AspNetUsers != _user)
                        return new TransactionResult(TransResult.Fail, "This is not your project", null);
                var projectPhoto = new ProjectPhoto
                {
                    Project1 = _project,
                    Photo = Convert.FromBase64String(projectPhotoDTO.PhotoString)
                };
                context.ProjectPhoto.Add(projectPhoto);
                await context.SaveChangesAsync();

                return new TransactionResult(TransResult.Success, "Success", null);                               
            }
            catch (Exception ex) { return new TransactionResult(TransResult.Fail, ex.Message, ex); }
        }

        public async Task<TransactionResult> DeleteImageTransaction(int Id, string user)
        {
            try
            {
                AspNetUsers _user = await context.AspNetUsers.Where(u => u.UserName == user).FirstOrDefaultAsync();
                ProjectPhoto projectPhoto = await context.ProjectPhoto.FindAsync(Id);
                Project project = await context.Project.FindAsync(projectPhoto.ProjectFK);
                if (project.AspNetUsers != _user)
                    return new TransactionResult(TransResult.Fail, "This is not your project", null);
                context.ProjectPhoto.Remove(projectPhoto);
                await context.SaveChangesAsync();
                return new TransactionResult(TransResult.Success, "Success", null);                 
            }
            catch (Exception ex) { return new TransactionResult(TransResult.Fail, ex.Message, ex); }
        }

        public async Task<TransactionResult> SetMainPhotoTransaction(int Id, string user)
        {
            try
            {
                AspNetUsers _user = await context.AspNetUsers.Where(u => u.UserName == user).FirstOrDefaultAsync();
                if(_user == null) return new TransactionResult(TransResult.Fail, "You are not authorized", null);

                ProjectPhoto projectPhoto = await context.ProjectPhoto.FindAsync(Id);
                Project project = await context.Project.FindAsync(projectPhoto.ProjectFK);
                if (project.AspNetUsers != _user)
                    return new TransactionResult(TransResult.Fail, "This is not your project", null);

                project.ProjectPhoto = projectPhoto;
                await context.SaveChangesAsync();

                return new TransactionResult(TransResult.Success, "Success", null);
                
            }
            catch (Exception ex) { return new TransactionResult(TransResult.Fail, ex.Message, ex); }
        }

        public async Task<TransactionResult> ReadProjectImages(int Id)
        {
            try
            {
                var result = new List<ProjectPhotoDTO>();
                result = await context.ProjectPhoto.Where(a => a.ProjectFK == Id).Select(s => new ProjectPhotoDTO
                {
                    Id = s.Id,
                    Photo = s.Photo
                }).ToListAsync();
                return new TransactionResult(TransResult.Success, string.Empty, result);
            }
            catch (Exception ex) { return new TransactionResult(TransResult.Fail, ex.Message, ex); }
        }

        public async Task<TransactionResult> ReadProjectMainImage(int id, int? pointer = null)
        {
            try
            {
                Project project = await context.Project.FindAsync(id);                    
                var photo = await context.ProjectPhoto.FindAsync(project.MainPhotoFK);
                var result = new ProjectPhotoDTO
                {
                    Id = photo.Id,
                    Photo = photo.Photo,
                    ProjectFK = project.Id,
                    Pointer = pointer
                };
                return new TransactionResult(TransResult.Success, string.Empty, result);
                
            }
            catch (Exception ex) { return new TransactionResult(TransResult.Fail, ex.Message, ex); }
        }
    }
}
