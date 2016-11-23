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
                using (var db = new backup_CrowdFundingViva1Entities())
                {
                
                    AspNetUsers _user = await db.AspNetUsers.Where(u => u.UserName == user).FirstOrDefaultAsync();

                    Project _project = null;
                    if (projectPhotoDTO.Id != null)
                        _project = await db.Project.FindAsync(projectPhotoDTO.Id);

                    if (_project != null)
                        if (_project.AspNetUsers != _user)
                            return new TransactionResult(TransResult.Fail, "This is not your project", null);

                    var projectPhoto = new ProjectPhoto
                    {
                        Project1 = _project,
                        Photo = Convert.FromBase64String(projectPhotoDTO.PhotoString)
                    };

                    db.ProjectPhoto.Add(projectPhoto);
                    await db.SaveChangesAsync();

                    return new TransactionResult(TransResult.Success, "Success", null);
                }                
            }
            catch (Exception ex) { return new TransactionResult(TransResult.Fail, ex.Message, ex); }
        }

        public async Task<TransactionResult> DeleteImageTransaction(int Id, string user)
        {
            try
            {
                using (var db = new backup_CrowdFundingViva1Entities())
                {

                    AspNetUsers _user = await db.AspNetUsers.Where(u => u.UserName == user).FirstOrDefaultAsync();

                    ProjectPhoto projectPhoto = await db.ProjectPhoto.FindAsync(Id);
                    Project project = await db.Project.FindAsync(projectPhoto.ProjectFK);
                    if (project.AspNetUsers != _user)
                        return new TransactionResult(TransResult.Fail, "This is not your project", null);

                    db.ProjectPhoto.Remove(projectPhoto);
                    await db.SaveChangesAsync();

                    return new TransactionResult(TransResult.Success, "Success", null);
                }
            }
            catch (Exception ex) { return new TransactionResult(TransResult.Fail, ex.Message, ex); }
        }

        public async Task<TransactionResult> SetMainPhotoTransaction(int Id, string user)
        {
            try
            {
                using (var db = new backup_CrowdFundingViva1Entities())
                {

                    AspNetUsers _user = await db.AspNetUsers.Where(u => u.UserName == user).FirstOrDefaultAsync();
                    if(_user == null) return new TransactionResult(TransResult.Fail, "You are not authorized", null);

                    ProjectPhoto projectPhoto = await db.ProjectPhoto.FindAsync(Id);
                    Project project = await db.Project.FindAsync(projectPhoto.ProjectFK);
                    if (project.AspNetUsers != _user)
                        return new TransactionResult(TransResult.Fail, "This is not your project", null);

                    project.ProjectPhoto = projectPhoto;
                    await db.SaveChangesAsync();

                    return new TransactionResult(TransResult.Success, "Success", null);
                }
            }
            catch (Exception ex) { return new TransactionResult(TransResult.Fail, ex.Message, ex); }
        }

        public async Task<TransactionResult> ReadProjectImages(int Id)
        {
            try
            {
                using (var db = new backup_CrowdFundingViva1Entities())
                {
                    var result = new List<ProjectPhotoDTO>();
                    result = await db.ProjectPhoto.Where(a => a.ProjectFK == Id).Select(s => new ProjectPhotoDTO
                    {
                        Id = s.Id,
                        Photo = s.Photo
                    }).ToListAsync();
                    return new TransactionResult(TransResult.Success, string.Empty, result);
                }
            }
            catch (Exception ex)
            {
                return new TransactionResult(TransResult.Fail, ex.Message, ex);
            }
        }

        public async Task<TransactionResult> ReadProjectMainImage(int id, int? pointer = null)
        {
            try
            {
                using (var db = new backup_CrowdFundingViva1Entities())
                {
                    Project project = await db.Project.FindAsync(id);                    
                    var photo = await db.ProjectPhoto.FindAsync(project.MainPhotoFK);
                    var result = new ProjectPhotoDTO
                    {
                        Id = photo.Id,
                        Photo = photo.Photo,
                        ProjectFK = project.Id,
                        Pointer = pointer
                    };
                    return new TransactionResult(TransResult.Success, string.Empty, result);
                }
            }
            catch (Exception ex)
            {
                return new TransactionResult(TransResult.Fail, ex.Message, ex);
            }
        }
    }
}
