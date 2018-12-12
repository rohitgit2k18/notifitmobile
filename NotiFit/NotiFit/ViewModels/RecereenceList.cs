using NotiFit.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotiFit.ViewModels
{
   public  class RecereenceList
    {
        public List<RecerrenceListEntity> RecerrenceSource { get; }
     = new List<RecerrenceListEntity>() {
                new RecerrenceListEntity() {
                   RecerrenceID=1,
                   RecerrenceName="Every week"
                },
                new RecerrenceListEntity() {
                    RecerrenceID=2,
                   RecerrenceName="Every two weeks"
                },
                new RecerrenceListEntity() {
                   RecerrenceID=3,
                   RecerrenceName="Every three weeks"
                },
                new RecerrenceListEntity() {
                    RecerrenceID=4,
                   RecerrenceName="Every four weeks"
                }
                             
                
         };
    }
}
