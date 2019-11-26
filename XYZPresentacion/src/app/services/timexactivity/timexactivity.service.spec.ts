import { TestBed } from '@angular/core/testing';

import { TimexactivityService } from './timexactivity.service';

describe('TimexactivityService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: TimexactivityService = TestBed.get(TimexactivityService);
    expect(service).toBeTruthy();
  });
});
