import { TestBed } from '@angular/core/testing';

import { RegisterShipmentService } from './register-shipment.service';

describe('RegisterShipmentService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: RegisterShipmentService = TestBed.get(RegisterShipmentService);
    expect(service).toBeTruthy();
  });
});
