import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ViviendaCreateFormComponent } from './vivienda-create-form.component';


describe('ViviendaCreateFormComponent', () => {
  let component: ViviendaCreateFormComponent;
  let fixture: ComponentFixture<ViviendaCreateFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ViviendaCreateFormComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ViviendaCreateFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
