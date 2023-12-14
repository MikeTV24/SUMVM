﻿using SUMVM.Models.Domain;
using SUMVM.Models.DTO;
using System.Collections;

namespace SUMVM.Repositories.Abstract
{

    public interface IMovieService
    {
        bool Add(Movie model);
        bool Update(Movie model);
        Movie GetById(int id);
        bool Delete(int id);
        MovieListVm List(string term = "", bool paging = false, int currentPage = 0);
        List<int> GetGenreByMovieId(int movieId);

    }
}