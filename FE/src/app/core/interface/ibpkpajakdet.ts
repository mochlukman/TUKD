import { IBpkpajak } from './ibpkpajak';
import { IPajak } from './ipajak';

export interface IBpkpajakdet {
  idbpkpajakdet : number;
  idbpkpajak : number;
  idpajak : number;
  nilai : number;
  datecreate : null;
  dateupdate : null;
  idbpkpajakNavigation : IBpkpajak;
  idpajakNavigation : IPajak;
}
