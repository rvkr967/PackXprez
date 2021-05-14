export interface IRegistration {
  emailId: string,
  shippingType: string,
  length: number,
  breadth: number,
  height: number,
  weight: number,
  deliveryType: string,
  timeSlot: string,
  pickAddressId: number,
  deliveryAddress: string,
  packingService: boolean
}
