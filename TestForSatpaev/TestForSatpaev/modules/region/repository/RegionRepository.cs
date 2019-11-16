using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestForSatpaev.modules.region.dto;
using TestForSatpaev.modules.region.entity;

namespace TestForSatpaev.modules.region.repository
{
    public class RegionRepository
    {
        RegionMapper regionMapper;
        public RegionRepository()
        {
            regionMapper = new RegionMapper();
        }
        public async Task<Region> GetRegionWithoutChilds(string name)
        {
            using (Context db = new Context())
            {
                Region ret = await (from r in db.Regions
                                    where r.Name == name
                                    select r).FirstOrDefaultAsync();
                return ret;
            }
        }
        public async Task<Region> GetRegionWithChilds(string name)
        {
            using(Context db = new Context())
            {
                Region ret = await (from r in db.Regions.Include(x => x.ChildRegs).ThenInclude(x => x.ChildRegs)
                        where r.Name == name
                        select r).FirstOrDefaultAsync();
                return ret;
            }
        }
        public async Task<List<Region>> GetAllRegions()
        {
            using(Context db = new Context())
            {
                return await (from r in db.Regions.Include(x => x.ChildRegs).ThenInclude(x => x.ChildRegs)
                              select r).ToListAsync();
            }
        }

        public async Task<Region> SaveSingleRegion(RegionDto dto)
        {
            using(Context db = new Context())
            {
                Region region = regionMapper.Cast(dto);
                db.Entry(region).State = EntityState.Added;
                await db.SaveChangesAsync();
                await db.Entry(region).ReloadAsync();
                return region;
            }
        }

        public async Task<Region> SaveRegionWithChilds(RegionDto dto, List<Region> childs)
        {
            using(Context db = new Context())
            {
                Region region = new Region { Name = dto.Name, ChildRegs = childs };
                db.Entry(region).State = EntityState.Added;
                foreach (Region r in childs)
                    db.Entry(r).State = EntityState.Modified;
                await db.SaveChangesAsync();
                await db.Entry(region).ReloadAsync();
                return region;
            }
        }

        public async Task<bool> Delete(string name)
        {
            using(Context db = new Context())
            {
                Region region = db.Regions.FirstOrDefault(r => r.Name == name);
                if (region == null)
                    return true;
                db.Entry(region).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> SaveRegion(RegionDto dto)
        {
            throw new NotImplementedException();
            using(Context db = new Context())
            {
                void ChangeEntityStateForChilds(Region ef)
                {
                    db.Entry(ef).State = EntityState.Added;
                    foreach(Region r in ef.ChildRegs)
                    {
                        ChangeEntityStateForChilds(r);
                    }
                }
                Region region = await GetRegionWithChilds(dto.Name);
                if (region != null)
                    throw new Exception("Region with same name already exists");
                region = regionMapper.Cast(dto);
                if(region.ChildRegs != null && region.ChildRegs.Count > 0)
                {
                    ChangeEntityStateForChilds(region);
                }
                await db.SaveChangesAsync();
                return true;
            }
        }
    }
}
