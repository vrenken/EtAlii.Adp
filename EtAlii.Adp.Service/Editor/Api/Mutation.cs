using Microsoft.EntityFrameworkCore;

namespace EtAlii.Adp.Service;

// ReSharper disable once ClassNeverInstantiated.Global
public partial class Mutation 
{
    public async Task<View> AddView([Service] DbContext dbContext, View view)
    {
        await dbContext.Views.AddAsync(view);
        return view;
    }
}