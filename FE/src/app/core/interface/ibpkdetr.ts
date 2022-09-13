import { IBpk } from './ibpk';
import { IDaftrekening } from './idaftrekening';
import { IJtrnlkas } from './ijtrnlkas';

export interface IBpkdetr {
  idbpkdetr : number;
  idbpk : number;
  idkeg : number;
  idrek : number;
  idnojetra : number;
  datecreate : null;
  nilai : number;
  dateupdate : null;
  idbpkNavigation : IBpk;
  idnojetraNavigation : IJtrnlkas;
  idrekNavigation : IDaftrekening;
}