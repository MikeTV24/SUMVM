﻿using SUMVM.Models.Domain;
using SUMVM.Models.DTO;

namespace SUMVM.Repositories.Abstract
{

    public interface IGenreService
    {
        bool Add(Genre model);
        bool Update(Genre model);
        Genre GetById(int id);
        bool Delete(int id);
        IQueryable<Genre> List();

    }
}