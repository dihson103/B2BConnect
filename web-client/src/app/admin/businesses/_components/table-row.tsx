import { Badge } from '@/components/ui/badge'
import { TableCell, TableRow } from '@/components/ui/table'
import Image from 'next/image'
import envConfig from '@/config'
import { BusinessResponse } from '@/types/business.types'
import BusinessTableRowAction from '@/app/admin/businesses/_components/table-row-action'

type Props = {
  data: BusinessResponse
}

export default function BusinessTableRow({ data }: Props) {
  return (
    <TableRow>
      <TableCell className='hidden sm:table-cell'>
        <Image
          alt='Product image'
          className='aspect-square rounded-md object-cover'
          height='30'
          src={`${envConfig.API_ENDPOINT}/files/${data.avatarImage ?? 'not-found.jpg'}`}
          width='30'
        />
      </TableCell>
      <TableCell className='font-medium'>{data.taxCode}</TableCell>
      <TableCell className='font-medium' width={250}>
        {data.name}
      </TableCell>
      <TableCell>
        <Badge variant='outline' className='hidden md:table-cell'>
          {data.numberOfEmployee}
        </Badge>
      </TableCell>
      <TableCell>{data.dateOfEstablishment}</TableCell>
      <TableCell>
        <BusinessTableRowAction id={data.id} />
      </TableCell>
    </TableRow>
  )
}
