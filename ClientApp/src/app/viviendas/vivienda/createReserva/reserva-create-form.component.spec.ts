import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ReservaCreateFormComponent } from './reserva-create-form.component';


describe('ReservaCreateFormComponent', () => {
  let component: ReservaCreateFormComponent;
  let fixture: ComponentFixture<ReservaCreateFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ReservaCreateFormComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReservaCreateFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
