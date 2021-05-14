import { TestBed } from '@angular/core/testing';

import { UserRegService } from './user-reg.service';

describe('UserRegService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: UserRegService = TestBed.get(UserRegService);
    expect(service).toBeTruthy();
  });
});
