import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TimexactivityComponent } from './timexactivity.component';

describe('TimexactivityComponent', () => {
  let component: TimexactivityComponent;
  let fixture: ComponentFixture<TimexactivityComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TimexactivityComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TimexactivityComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
