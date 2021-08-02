using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StoreFront.DATA.EF;
using System.ComponentModel.DataAnnotations;

namespace StoreFront2._0.Models
{
    public class CartItemViewModel
    {
        [Range(1,int.MaxValue)]
        public int Quantity { get; set; }
        public VideoGame Game { get; set; }

        public CartItemViewModel(int quantity, VideoGame game)
        {
            Quantity = quantity;
            Game = game;
        }

    }
}