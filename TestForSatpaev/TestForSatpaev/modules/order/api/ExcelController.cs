using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TestForSatpaev.modules.order.dto;
using TestForSatpaev.modules.order.service;

namespace TestForSatpaev.modules.order.api
{
    [Route("api/[controller]")]
    //[Authorize(Roles = "admin")]
    [ApiController]
    public class ExcelController
    {
        private OrderService orderService;
        public ExcelController()
        {
            orderService = new OrderService();
        }
        [HttpGet]
        public async Task<HttpResponseMessage> GetOrdersInExcel()
        {
            List<OrderDto> orders = await orderService.GetAllOrders();
            XLWorkbook wb = new XLWorkbook();
            wb.PageOptions.PagesWide = 1;
            wb.Worksheets.Add(ConvertToDatatable(orders), "Заявки в ЛИС");
            var stream = new MemoryStream();
            wb.SaveAs(stream);

            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new ByteArrayContent(stream.ToArray());
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/download");
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = "Список заявок в ЛИС.xlsx" };
            return response;
        }
        public static DataTable ConvertToDatatable<T>(IEnumerable<T> data)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                var prop = props[i];
                if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    table.Columns.Add(prop.Description, prop.PropertyType.GetGenericArguments()[0]);
                else
                    table.Columns.Add(prop.Description, prop.PropertyType);
            }

            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }
    }
}
