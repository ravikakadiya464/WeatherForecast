import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TempratureSwitchComponent } from './temprature-switch.component';

describe('TempratureSwitchComponent', () => {
  let component: TempratureSwitchComponent;
  let fixture: ComponentFixture<TempratureSwitchComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TempratureSwitchComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TempratureSwitchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
