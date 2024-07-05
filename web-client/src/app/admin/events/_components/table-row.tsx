import { Event } from '@/types/event.types'
import { Badge } from '@/components/ui/badge'
import { TableCell, TableRow } from '@/components/ui/table'
import Image from 'next/image'
import UpdateEventForm from '@/app/admin/events/_components/update-event-form'
import envConfig from '@/config'

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
          src={`${envConfig.API_ENDPOINT}/files/${data.image}`}
          width='30'
        />
      </TableCell>
      <TableCell className='font-medium'>{data.name}</TableCell>
      <TableCell>
        <Badge variant='outline'>{data.statusDescription}</Badge>
      </TableCell>
      <TableCell className='font-medium'>{data.location}</TableCell>
      <TableCell className='hidden md:table-cell'>{data.startAt}</TableCell>
      <TableCell className='hidden md:table-cell'>{data.endAt}</TableCell>
      <TableCell>
        <UpdateEventForm key={data.id} data={data} />
      </TableCell>
    </TableRow>
  )
}
