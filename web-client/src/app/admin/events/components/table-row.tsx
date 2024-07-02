import { Event } from '@/types/event.types'
import { Badge } from '@/components/ui/badge'
import { TableCell, TableRow } from '@/components/ui/table'
import Image from 'next/image'
import EventTableRowAction from '@/app/admin/events/components/table-row-action'

type Props = {
  data: Event
}

export default function EventTableRow({ data }: Props) {
  return (
    <TableRow>
      <TableCell className='hidden sm:table-cell'>
        <Image
          alt='Product image'
          className='aspect-square rounded-md object-cover'
          height='30'
          src={'/image_not_found.png'}
          width='30'
        />
      </TableCell>
      <TableCell className='font-medium'>{data.name}</TableCell>
      <TableCell>
        <Badge variant='outline'>{data.statusDescription}</Badge>
      </TableCell>
      <TableCell className='font-medium'>{data.location}</TableCell>
      <TableCell className='hidden md:table-cell'>{data.startDate}</TableCell>
      <TableCell className='hidden md:table-cell'>{data.endDate}</TableCell>
      <TableCell>
        <EventTableRowAction id={data.id} />
      </TableCell>
    </TableRow>
  )
}
