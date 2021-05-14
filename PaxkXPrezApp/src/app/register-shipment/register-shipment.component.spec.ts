import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RegisterShipmentComponent } from './register-shipment.component';

describe('RegisterShipmentComponent', () => {
  let component: RegisterShipmentComponent;
  let fixture: ComponentFixture<RegisterShipmentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RegisterShipmentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RegisterShipmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
