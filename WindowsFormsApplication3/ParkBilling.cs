using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUB {
    class ParkBilling {
        int parkId;

        public ParkBilling(int parkId) {
            this.parkId = parkId;
        }

        public void generateBill() {
            String[] fields = { "ParkSpaceID" };
            String condition = "ParkID=@value0 AND MoveOutDate IS NULL";
            ArrayList spaces = DatabaseControl.getMultipleRecord(fields, DatabaseControl.spaceTable, condition, new Object[] { parkId });
            foreach (Object[] space in spaces) {
                int spaceId = (int)space[0];
                TenantBill.TenantBilling bill = new TenantBill.TenantBilling(parkId, spaceId);
                bill.generateBill();
            }
        }
    }
}
